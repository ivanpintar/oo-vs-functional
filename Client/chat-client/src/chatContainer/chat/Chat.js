import React from 'react'
import { connect } from 'react-redux'
import MessageList from './MessageList'
import InputBox from './InputBox'
import { bindActionCreators } from 'redux'
import * as chatActions from './chatActions'
import { Button } from 'react-bootstrap'

const Chat = ({chat, currentUser, sendMessageAction, leaveChatAction}) => {
    if(!chat) return null;

    const participantList = chat.participants.join(', ');

    return (
        <div className="well">
            <h3>{chat.name} <small>{participantList}</small></h3>            
            <InputBox currentUser={currentUser} chatName={chat.name} sendMessageAction={sendMessageAction}/>
            <MessageList messages={chat.messages}/>            
            <Button onClick={() => leaveChatAction(chat.name, currentUser)}>Leave</Button>
        </div>        
    )
}

const mapStateToProps = (state) => ({ 
    chat: state.chats.find((c) => c.selected), 
    currentUser: state.currentUser 
})

const mapDispatchToProps = (dispatch) => bindActionCreators({ ...chatActions }, dispatch)

export default connect(mapStateToProps, mapDispatchToProps)(Chat);