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
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Select a username:
                <input type="text" value={this.state.value} onChange={this.handleChange} />
                </label>
                <input type="submit" value="Submit" />
            </form>
        );
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ ...loginActions }, dispatch);
}

export default connect(null, mapDispatchToProps)(LoginScreen)
