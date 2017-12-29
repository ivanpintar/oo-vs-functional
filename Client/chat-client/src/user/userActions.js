export const loginAction = (username) => ({ type : 'USER.LOGIN', username })
export const loggedInAction = (username) => ({ type : 'USER.LOGGED_IN', username })
export const logoutAction = (username) => ({ type : 'USER.LOGOUT', username })
export const loggedOutAction = (username) => ({ type : 'USER.LOGGED_OUT', username })