export default function loginReducer(userName = '', action) {
    switch(action.type) {
        case 'LOGIN': 
            return action.userName;
        default:
            return userName;
    }
}