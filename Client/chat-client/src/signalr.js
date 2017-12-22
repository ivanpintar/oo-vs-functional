
import * as SignalR from '@aspnet/signalr-client'
import { chatCreatedAction } from './chatContainer/chatList/chatListActions'
import { messageReceivedAction, leftChatAction } from './chatContainer/chat/chatActions'

const connection = new SignalR.HubConnection('/chatHub')

export default function registerSignalrHandlers(store, callback) {
    connection.on('ChatCreated', chat => {
        store.dispatch(chatCreatedAction(chat.name))
    })

    connection.on('MessageSent', message => {
        store.dispatch(messageReceivedAction(message.chatName, message.from, message.text, message.order))
    })

    connection.on('ChatLeft', left => {
        store.dispatch(leftChatAction(left.chatName, left.participant))
    });
}