import { combineEpics } from 'redux-observable'
import { loggedInAction, loggedOutAction, userExistsAction } from './userActions'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'
import constants from '../constants'

const loginUrl = constants.apiUrl + 'account/login';
const logoutUrl = constants.apiUrl + 'account/logout';

const loginEpic = (action$) => 
    action$.ofType('USER.LOGIN')
        .mergeMap(a => 
            ajax.post(loginUrl, { username: a.username }, { 'Content-Type': 'application/json'})
                .map(response => loggedInAction(a.username))
                .catch(error => Observable.of(userExistsAction(a.username)))
        )

const logoutEpic = (action$) => 
    action$.ofType('USER.LOGOUT')
        .mergeMap(a => 
            ajax.post(logoutUrl, { username: a.username }, { 'Content-Type': 'application/json'})
                .map(response => loggedOutAction(a.username))
        )

export default combineEpics(loginEpic, logoutEpic)