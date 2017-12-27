export default function userReducer(currentUser = '', action) {
    switch(action.type) {
        case 'USER.LOGGED_IN': 
            return action.username;
        case 'USER.LOGGED_OUT': 
            return '';
        case 'USER.USER_EXISTS':
            alert('Username "' + action.username + '" is taken');
            return currentUser; 
        case 'USER.USERNAME_INVALID':
            alert('Username is invalid');
            return currentUser; 
        default:
            return currentUser;
    }
}