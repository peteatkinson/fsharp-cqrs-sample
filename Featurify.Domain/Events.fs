module Events
open Models

type State = {
    isActive: bool
} with static member Zero = {isActive =  false}

type Command = 
    | CreateIdea of FeatureIdea

type Event = 
    | FeatureIdeaCreated of FeatureIdea

let apply item = function
    | FeatureIdeaCreated _ -> {item with State.isActive = true; }


let exec state = 
    function 
    | CreateIdea item ->  FeatureIdeaCreated(item)
