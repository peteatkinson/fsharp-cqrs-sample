module Models
open System

type FeatureId = FeatureId of Guid

type FeatureIdea = {
    Id: FeatureId
    Title: string
}

type RoadMapId = RoadMapId of Guid

type RoadMap = {
    Id: RoadMapId
    Title: string
}

type ProjectId = ProjectId of Guid

type Project = {
    Id: ProjectId
    Title: string
}

type CommentId = CommentId of Guid

type Comment = {
    Id: CommentId
    Text: string
}
