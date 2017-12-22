export const sendMessageAction = (chatName, from, text) => ({ type: 'CHAT.CHAT.SEND_MESSAGE', chatName, from, text })
export const messageReceivedAction = (chatName, from, text, order) => ({ type: 'CHAT.CHAT.MESSAGE_RECEIVED', chatName, from, text, order })
export const leaveChatAction = (chatName, userThatLeft) => ({ type: 'CHAT.CHAT.LEAVE', chatName, userThatLeft })
export const leftChatAction = (chatName, userThatLeft) => ({ type: 'CHAT.CHAT.LEFT', chatName, userThatLeft })
export const chatLoadedAction = (chat) => ({ type: 'CHAT.CHAT.LOADED', chat })