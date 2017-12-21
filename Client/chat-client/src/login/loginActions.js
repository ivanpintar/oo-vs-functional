export const loginAction = (username) => ({ type : 'LOGIN.LOGIN', username })
export const loggedInAction = (username) => ({ type : 'LOGIN.LOGGED_IN', username })
export const userExistsAction = (username) => ({ type : 'LOGIN.USER_EXISTS', username })