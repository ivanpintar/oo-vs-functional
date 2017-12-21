import React from 'react'
import { Button, InputGroup, FormControl } from 'react-bootstrap'

export default class CreateChat extends React.Component {
    constructor(props) {
        super(props);

        this.state = { value: '' };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    handleSubmit(event) {
        this.props.createChatAction(this.state.value);
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
            <div>
                <InputGroup>
                    <FormControl type="text" value={this.state.value} onChange={this.handleChange} placeholder="Enter chat name to create a new chat" onKeyPress={this.handleKeyPress} />
                    <InputGroup.Button>
                        <Button onClick={this.handleSubmit}>Add Chat</Button>
                    </InputGroup.Button>
                </InputGroup>
            </div>
        );
    }
}
