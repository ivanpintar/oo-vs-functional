import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import * as chatListActions from './chatListActions'
import CreateChat from './CreateChat'
import { Button, ButtonToolbar } from 'react-bootstrap'

const ChatList = ({chats, createChatAction, selectChatAction}) => {

    const createChatName = (c) => <Button key={c.name}  onClick={() => selectChatAction(c.name)}>{c.name}</Button>
    const chatNames = chats.map(createChatName);    

    return (
        <div> 
            <div className="well">
                <CreateChat createChatAction={createChatAction}/>
                <ButtonToolbar style={{ padding: '10px 0' }}>{chatNames}</ButtonToolbar>
            </div>
        </div>    
    )
}

function mapStateToProps(state) {
    return {
        chats: state.chats
    }
}

function mapActionsToProps(dispatch) {
    return bindActionCreators({ ...chatListActions }, dispatch)
}

export default connect(mapStateToProps, mapActionsToProps)(ChatList)