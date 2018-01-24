module Handlers

let conn = EventStore.conn "127.0.0.1"

//let handleCommand' =
   // Aggregates.makeHandler 
     //   { zero = Events.State.Zero; apply = Events.apply; exec = Events.exec }
      // (EventStore.makeRepository conn "InventoryItem" Serializer.serializer)
