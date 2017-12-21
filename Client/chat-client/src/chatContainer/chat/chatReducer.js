import { Message } from '../../models'

export default function chatReducer(chat, action) {
    switch(action.type) {
        case "CHAT.CHAT.MESSAGE_RECEIVED": {
            if(!action.text) return chat;

            const messages = chat.messages.push(new Message({ 
                order: action.order,
                from: action.from,
                text: action.text
            }))

            const participantExists = chat.participants.find((p) => p === action.from);
            const participants = participantExists ? chat.participants : chat.participants.push(action.from);

            return chat.merge({ messages, participants });
        }
        case "CHAT.CHAT.LEFT": {
            alert(action.userThatLeft + ' left ' + chat.name)
            const participants = chat.participants.filter(p => p === action.userThatLeft);
            return chat.merge({ participants });  
        }
        default:
            return chat;
    }
}