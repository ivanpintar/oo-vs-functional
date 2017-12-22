
import * as SignalR from '@aspnet/signalr-client'
import { chatCreatedAction } from './chatContainer/chatList/chatListActions'
import { messageReceivedAction, leftChatAction } from './chatContainer/chat/chatActions'
import constants from './constants'

const connection = new SignalR.HubConnection(constants.chatHubUrl)

export default function registerSignalRHandlers(store, callback) {
    connection.on('chatCreated', chat => {
        store.dispatch(chatCreatedAction(chat.name))
    })

    connection.on('messageSent', message => {
        store.dispatch(messageReceivedAction(message.chatName, message.from, message.text, message.order))
    })

    connection.on('chatLeft', left => {
        store.dispatch(leftChatAction(left.chatName, left.participant))
    });

    connection.start().then(callback());
}