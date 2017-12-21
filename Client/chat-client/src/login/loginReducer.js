export default function loginReducer(currentUser = '', action) {
    switch(action.type) {
        case 'LOGIN.LOGGED_IN': 
            return action.username;
        case 'LOGIN.USER_EXISTS':
            alert('Username "' + action.username + '" is taken');
            return currentUser; 
        default:
            return currentUser;
    }
}