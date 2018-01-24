module EventStore
    open EventStore.ClientAPI
    open EventStore.ClientAPI.SystemData
    open System
    open System.Net

    let conn endPoint =   
        let config = ConnectionSettings.Create().SetDefaultUserCredentials(UserCredentials("admin", "changeit")).Build()
        let conn = EventStoreConnection.Create(config, new IPEndPoint(IPAddress.Parse(endPoint), 1113))
        conn.ConnectAsync() |> Async.AwaitTask |>  Async.RunSynchronously
        conn

    let makeRepository (conn:IEventStoreConnection) category (serialize:obj -> string * byte array, deserialize: Type * string * byte array -> obj) =
        let streamId (id:Guid) = category

        let load (t,id) = async {
            let streamId = streamId id
            let! eventsSlice = conn.ReadStreamEventsForwardAsync(streamId, int64(1), Int32.MaxValue, false) |> Async.AwaitTask
            return eventsSlice.Events |> Seq.map (fun e -> deserialize(t, e.Event.EventType, e.Event.Data))
        }

        let commit (id,expectedVersion) e = async {
            let streamId = streamId id
            let eventType,data = serialize e
            let metaData = [||] : byte array
            let eventData = new EventData(Guid.NewGuid(), eventType, true, data, metaData)
            let version = int64(ExpectedVersion.Any)
            if expectedVersion = 0 then conn.AppendToStreamAsync(streamId, version, eventData) |> Async.AwaitIAsyncResult |> Async.Ignore |> ignore
            return! conn.AppendToStreamAsync(streamId, int64(expectedVersion), eventData) |> Async.AwaitIAsyncResult |> Async.Ignore
        }

        load, commit