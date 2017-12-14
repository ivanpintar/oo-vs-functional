import React from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import * as loginActions from './loginActions'

class LoginScreen extends React.Component {
    constructor(props) {
        super(props);

        this.state = { value: '' }
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    handleSubmit(event) {
        this.props.loginAction(this.state.value);
        event.preventDefault();
    }

    render() {
        if(!this.props.visible) return null;

        return (
            <form onSubmit={this.handleSubmit}>
                <input type="text" value={this.state.value} onChange={this.handleChange} placeholder="Select a username"/>
                <input type="submit" value="Submit" />
            </form>
        );
    }
}

function mapStateToProps(state) {
    return {
        visible: state.userName === ""
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ ...loginActions }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginScreen)
