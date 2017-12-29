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

    let getChats () =
        chatList 

    let getChat name =
        try List.find (fun u -> u.Name = name) chatList |> Some
        with _ -> None

    let addChat chat =
        chatList <- chatList @ [ chat ]
        chat

    let addMessage chatName message =
        let addMessageFunction chat = 
            let newMessage = { message with Order = List.length chat.Messages }
            let newMessages = chat.Messages @ [ newMessage ]
            { chat with Messages = newMessages }
        updateChatInList addMessageFunction chatName

    let addParticipant chatName user = 
        let addParticipantFunction chat =
            let newParticipants = chat.Participants @ [ user ]
            { chat with Participants = newParticipants }
        updateChatInList addParticipantFunction chatName

    let removeParticipant chatName user =  
        let addParticipantFunction chat =
            let newParticipants = List.filter (fun p -> p.Username <> user.Username) chat.Participants
            { chat with Participants = newParticipants }
        updateChatInList addParticipantFunction chatName