import React from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import * as loginActions from './loginActions'
import { InputGroup, FormControl, Button } from 'react-bootstrap'

class LoginScreen extends React.Component {
    constructor(props) {
        super(props);

        this.state = { value: '' }
        this.handleChange = this.handleChange.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    handleSubmit(event) {
        this.props.loginAction(this.state.value);
        event.preventDefault();
    }

    handleKeyPress(event) {
        if(event.key === 'Enter') {
            this.handleSubmit(event);
        }
    }

    render() {
        if(!this.props.visible) return null;

        return (
            <div className="well">
                <InputGroup>
                    <FormControl type="text" value={this.state.value} onChange={this.handleChange} placeholder="Enter a username to login" onKeyPress={this.handleKeyPress} />
                    <InputGroup.Button>
                        <Button onClick={this.handleSubmit}>Login</Button>
                    </InputGroup.Button>
                </InputGroup>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        visible: state.currentUser === ""
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ ...loginActions }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginScreen)
