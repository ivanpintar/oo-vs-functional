import { List } from 'immutable'
import { Chat } from '../../models'

export default function chatListReducer(chatList = List(), action) {
    switch(action.type) {
        case 'CHAT.LIST.ADD': 
            var chat = chatList.find(c => c.name === action.name);
            if(chat) {
                alert('Chat ' + action.name + ' already exists');
                return chatList;
            }

            return chatList.push(new Chat({
                name: action.name,
                active: true,
                selected: false
            }));
        case 'CHAT.LIST.SELECT':
            return chatList.map(c => {
                if(c.name === action.name){
                    return c.set('selected', true)
                } 
                return c.set('selected', false);
            });
        default:
            return chatList;
    }
}