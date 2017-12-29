export default function userReducer(currentUser = '', action) {
    switch(action.type) {
        case 'USER.LOGGED_IN': 
            return action.username;
        case 'USER.LOGGED_OUT': 
            return '';
        default:
            return currentUser;
    }
}