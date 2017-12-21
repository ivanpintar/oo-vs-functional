import React from 'react'
import { connect } from 'react-redux'
import ChatList from './chatList/ChatList'
import Chat from './chat/Chat'

const ChatContainer = ({visible, currentUser}) => {
    if(!visible) return null;

    return (
        <div>
            <div style={{padding: '10px'}}>Logged in as: {currentUser}</div>
            <ChatList />
            <Chat />
        </div>
    )
}

function mapStateToProps(state) {
    return {
        visible: state.currentUser !== "",
        currentUser: state.currentUser
    }
}

export default connect(mapStateToProps, null)(ChatContainer)