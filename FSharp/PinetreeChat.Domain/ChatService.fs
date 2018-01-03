namespace PinetreeChat.Domain

module ChatService = 
    open PinetreeChat.Entities
    open Validation
    open Chessie.ErrorHandling   

    type ChatErrorMessage = 
        | ChatNameInvalid of string
        | ChatExists of string
        | MessageInvalid of string

    let getErrorMessage e =
        match e with
        | ChatNameInvalid msg -> msg
        | ChatExists msg -> msg
        | MessageInvalid msg -> msg
    
    let createChat findChatFunction addChatFunction chatName = 
        let notEmpty = validateEmptyString (ChatNameInvalid "Chat name is empty")
        let lengthValid = validateStringLength 1 50 (ChatNameInvalid "Chat name is too long")
        let validateChatName cn = notEmpty cn <* lengthValid cn

        let createChat cn =
            match findChatFunction cn with
            | Some _ -> Bad [ (ChatExists "A chat with this name already exists") ]
            | None ->   
                { Id = 0; Name = cn; Messages = []; Participants = [] }
                |> addChatFunction
                |> ok

        chatName |> validateChatName >>= createChat

    let addMessage (findUserFunction:string -> User option) (addMessageFunction:string -> Message -> (Chat * Message)) (addParticipantFunction:string -> User -> (Chat * User)) chatName text from =
        let notEmtpy = validateEmptyString (MessageInvalid "Message is empty")
        let lengthValid = validateEmptyString (MessageInvalid "Message is too long")
        let validateMessageText tx = notEmtpy tx <* lengthValid tx
        
        let createMessage cn un tx = 
            findUserFunction un
            |> fun(r) -> 
                match r with 
                | Some u -> ok u
                | None -> Bad [( MessageInvalid "No such user" )]
            |> lift (addParticipantFunction cn)
            |> lift (fun (_, u) -> { Id = 0; Order = 0; Text = tx; From = u})
            |> lift (fun (m) -> addMessageFunction cn m)

        text 
        |> validateMessageText 
        >>= createMessage chatName from        

module ChatServiceWithDataAccess =
    open PinetreeChat.DataAccess.ChatRepository
    open PinetreeChat.DataAccess.UserRepository
    open Chessie.ErrorHandling

    let getChat = getChat
    let getChats = getChats
    let createChat = ChatService.createChat getChat addChat
    let addMessage = ChatService.addMessage getUser addMessage addParticipant
    let leaveChat chatName userName = removeParticipant chatName userName |> ok
            