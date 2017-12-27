import { Record, List } from 'immutable'

export const State = new Record({
    currentUser: '',
    chats: List(), // Chat list
    error: ''
})

export const Chat = new Record({
    name: '',
    selected: false,
    loaded: false,
    participants: List(), // string list
    messages: List() // Message list
})

export const Message = new Record({
    order: 0,
    from: '',
    text: ''
})