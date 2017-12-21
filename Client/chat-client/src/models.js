import { Record, List } from 'immutable'

export const State = new Record({
    currentUser: '',
    chats: List() // Chat
})

export const Chat = new Record({
    name: '',
    selected: false,
    participants: List(), // string
    messages: List() // Message
})

export const Message = new Record({
    order: 0,
    from: '',
    text: ''
})