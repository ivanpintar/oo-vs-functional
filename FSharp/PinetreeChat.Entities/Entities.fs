module PinetreeChat.Entities

open System

type User = { Id: int; Username: string; IsLoggedIn: bool }
type Message = { Id: int; Order: int; From: User; Text: string }
type Chat = { Id: int; Name: string; Participants: User list; Messages: Message list }
