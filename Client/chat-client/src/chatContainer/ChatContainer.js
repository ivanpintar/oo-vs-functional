import React from 'react'
import { connect } from 'react-redux'
import ChatList from './chatList/ChatList'
import Chat from './chat/Chat'

const ChatContainer = ({visible}) => visible ? <div><ChatList /><Chat /></div> : null;

const mapStateToProps = (state) => ({ visible: state.currentUser !== "" })

export default connect(mapStateToProps, null)(ChatContainer)