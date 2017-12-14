import React from 'react'
import { connect } from 'react-redux'
import ChatList from './chatList/ChatList'
import Chat from './chat/Chat'

const ChatContainer = (props) => {
    if(!props.visible) return null;

    return <div><ChatList /><Chat /></div>
}

function mapStateToProps(state) {
    return {
        visible: state.userName !== ""
    }
}

export default connect(mapStateToProps, null)(ChatContainer)