import React from 'react'

export default class AddChat extends React.Component {
    constructor(props) {
        super(props);

        this.state = { value: '' };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    handleSubmit(event) {
        this.props.addChatAction(this.state.value);
        this.setState({ value: '' });
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <input type="text" value={this.state.value} onChange={this.handleChange} placeholder="Add chat" />
                <input type="submit" value="Submit" />
            </form>
        );
    }
}
