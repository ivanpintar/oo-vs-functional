function createAction(type, name) {
    return { type, name }
}

export function addChatAction(name) {
    return createAction('CHAT.LIST.ADD', name)
}

export function selectChatAction(name) {
    return createAction('CHAT.LIST.SELECT', name)
}