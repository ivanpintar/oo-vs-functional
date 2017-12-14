import { Record, List } from 'immutable'

export const State = new Record({
    userName: '',
    chats: List() // Chat
})

export const Chat = new Record({
    name: '',
    active: false,
    selected: false,
    participants: List(), // string
    messages: List() // Message
})

export const Message = new Record({
    from: '',
    text: ''
})