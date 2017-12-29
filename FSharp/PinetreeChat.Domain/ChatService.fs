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


    let getChat findChatFunction chatName =
        findChatFunction chatName
        
    let getChats findChatsFunction = findChatsFunction ()

    let createChat findChatFunction addChatFunction chatName = 
        let notEmpty = validateEmptyString (ChatNameInvalid "Chat name is empty")
        let lengthValid = validateStringLength 1 50 (ChatNameInvalid "Chat name is too long")
        let validateChatName cn = notEmpty cn <* lengthValid cn

        let createChat cn =
            match findChatFunction cn with
            | Some chat -> Bad [ (ChatExists "A chat with this name already exists") ]
            | None ->   
                { Id = 0; Name = cn; Messages = []; Participants = [] }
                |> addChatFunction
                |> ok

        chatName |> validateChatName >>= createChat

    let addMessage (findUserFunction:string -> User) (findChatFunction:string -> Chat) (addMessageFunction:string -> Message -> Message) (addParticipantFunction:string -> User -> User) chatName text from =
        let notEmtpy = validateEmptyString (MessageInvalid "Message is empty")
        let lengthValid = validateEmptyString (MessageInvalid "Message is too long")
        let validateMessageText tx = notEmtpy tx <* lengthValid tx
        
        let createMessage cn un tx =
            let user = findUserFunction un
            { Id = 0; Order = 0; From = user; Text = tx }
            |> addMessageFunction cn 
            |> ok

        text |> validateMessageText >>= createMessage chatName from
            