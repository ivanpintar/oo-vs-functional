import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import * as chatListActions from './chatListActions'
import AddChat from './AddChat'

const ChatList = ({chats, addChatAction, selectChatAction}) => {

    const createChatName = (c) => <button key={c.name} onClick={() => selectChatAction(c.name)}>{c.name}</button>;
    const chatNames = chats.map(createChatName);    

    return [ <div key="chatNames">{chatNames}</div>, 
             <AddChat key="addChat" addChatAction={addChatAction}/> ]
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