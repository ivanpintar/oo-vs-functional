import React from 'react'
import { FormControl, Button, InputGroup } from 'react-bootstrap'

export default class InputBox extends React.Component {
    constructor(props) {
        super(props);

        this.state = { value: '' }
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    handleSubmit(event) {
        this.props.sendMessageAction(this.props.chatName, this.props.currentUser, this.state.value);
        this.setState({ value: '' });
        event.preventDefault();
    }

    handleKeyPress(event) {
        if(event.key === 'Enter') {
            this.handleSubmit(event);
        }
    }

    render() {
        return (
            <InputGroup style={{ padding: '10px 0' }}>
                <FormControl type="text" value={this.state.value} onChange={this.handleChange} placeholder="Type your message here" onKeyPress={this.handleKeyPress}/>
                <InputGroup.Button>
                    <Button onClick={this.handleSubmit}>Send</Button>
                </InputGroup.Button>
            </InputGroup> 
        );
    }
}