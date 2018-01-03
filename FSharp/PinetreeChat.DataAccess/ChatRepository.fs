namespace PinetreeChat.DataAccess

module ChatRepository =
    open PinetreeChat.Entities    

    let mutable private chatList = List.empty<Chat>
    
    let private updateChatInList updateFunction name =
        let mappingFunction chat = 
            if (chat.Name = name)
            then updateFunction chat
            else chat           
            
        chatList <- List.map mappingFunction chatList
        List.find (fun u -> u.Name = name) chatList

    let private findChatInList name = List.find (fun u -> u.Name = name) chatList

    let getChats () =
        chatList 

    let getChat name =
        try findChatInList name |> Some
        with _ -> None

    let addChat chat =
        chatList <- chatList @ [ chat ]
        chat

    let addMessage chatName message =
        let chatMessageCount = 
            findChatInList chatName 
            |> fun (c) -> c.Messages.Length
        let newMessage = { message with Order = chatMessageCount }
        let addMessageFunction c = 
            let newMessages = c.Messages @ [ newMessage ]
            { c with Messages = newMessages }
        let chat = updateChatInList addMessageFunction chatName
        (chat, newMessage)

    let addParticipant chatName user = 
        let addParticipantFunction chat =
            let newParticipants = chat.Participants @ [ user ]
            { chat with Participants = newParticipants }
        let chat = updateChatInList addParticipantFunction chatName
        (chat, user)

    let removeParticipant chatName username =  
        let addParticipantFunction chat =
            let newParticipants = List.filter (fun p -> p.Username <> username) chat.Participants
            { chat with Participants = newParticipants }
        updateChatInList addParticipantFunction chatName