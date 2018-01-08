import { combineEpics } from 'redux-observable'
import { loggedInAction, loggedOutAction } from './userActions'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'
import { mergeMap } from 'rxjs' // eslint-disable-line
import constants from '../constants'
import { serverErrorAction } from '../app/appActions';

const loginUrl = constants.apiUrl + 'user/login';
const logoutUrl = constants.apiUrl + 'user/logout';

const loginEpic = (action$) => 
    action$.ofType('USER.LOGIN')
        .mergeMap(a => 
            ajax.post(loginUrl, { username: a.username }, { 'Content-Type': 'application/json'})
                .map(response => loggedInAction(a.username))
                .catch(error => Observable.of(serverErrorAction(error)))
        )

const logoutEpic = (action$) => 
    action$.ofType('USER.LOGOUT')
        .mergeMap(a => 
            ajax.post(logoutUrl, { username: a.username }, { 'Content-Type': 'application/json'})
                .map(response => loggedOutAction(a.username))
                .catch(error => Observable.of(serverErrorAction(error)))
        )

export default combineEpics(loginEpic, logoutEpic)