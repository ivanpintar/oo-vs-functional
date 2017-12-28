import { Message } from '../../models'
import { List } from 'immutable'

export default function chatReducer(chat, action) {
    switch(action.type) {
        case "CHAT.CHAT.LOADED": {
            const messages = action.chat.messages.map(m => new Message({ order: m.order, from: m.from, text: m.text }))
            return chat.merge({
                loaded: true,
                messages: List(messages),
                participants: List(action.chat.participants)
            })
        }
        case "CHAT.CHAT.MESSAGE_INVALID": {
            alert('Message is invalid');
            return chat;
        }
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
            const participants = chat.participants.filter(p => p !== action.userThatLeft);
            return chat.set('participants', participants);  
        }
        default:
            return chat;
    }
}