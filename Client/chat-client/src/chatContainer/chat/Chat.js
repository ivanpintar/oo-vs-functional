import React from 'react'
import { connect } from 'react-redux'

const Chat = ({chat}) => {
    if(!chat) return null;

    return <div>{chat.name}</div>
}

function mapStateToProps(state) {
    const chat = state.chats.find((c) => c.selected);
    return { chat }
}

export default connect(mapStateToProps, null)(Chat);