import Input from './Input';
import React from "react";
import users from './users';
import nameToLink from '../nameToLink';
import {
  Link,
} from "react-router-dom";
// import signImg from '../images/SignUp.jpg';
import signImg from '../images/SignUp.png';
import { Navigate } from "react-router-dom";
import './Login.css';
import serverUrl from '../ServerUrl';
import $ from 'jquery';
import JWT from './Jwt';

const Page = {
  LOGIN: 0,
  LOADING: 1,
  PASSED: 2
}
class Login extends React.Component {

  constructor(props) {
    super()
    this.state = {
      userName : 0, password : 0, error:'', page: Page.LOGIN
    };
    this.setUserName = this.setUserName.bind(this);
    this.setPassword = this.setPassword.bind(this);
    this.login = this.login.bind(this);   
    this.setPage = props.setPage;
    this.setUser = props.setUser;
  }

  async login() {
    if (!(this.state.userName && this.state.password)) {
      this.setState({error:'please enter user name and password'});
      return;
    }
    this.setState({page: Page.LOADING});
    const uName = this.state.userName;
    $.ajax({
      url: serverUrl + "api/Users/login",
      type: 'POST',
      cache: false,
      contentType: "application/json",
      data: JSON.stringify({
        "userName": uName,
        "password": this.state.password,
        "nickName": "string"
      }), 
      success: (data) => { // the JWT is in data
        JWT.JWT = data;
        this.setUser(uName);
        this.setPage(nameToLink.get('webPage'));
        this.setState({page: Page.PASSED});
      },
      error: (error) => {
        this.setState({page: Page.LOGIN, userName: "", password: "", error: error.responseText});
      }
    })
  }

  setUserName(value) {
    this.setState({
      userName: value
    })
  }
  setPassword(value) {
    this.setState({
      password: value
    })
  }

  render() {
    const regex = '^[a-zA-Z]';
    const error = 'must begin with a letter'


    const uNameElement = <Input inputSetter={this.setUserName} longInput={false} checkRegex={regex} type={'text'} howShortened={'1'}
    id={'uname'} description={'User Name:'} eDescription={error}/>;
    const passElement = <Input inputSetter={this.setPassword} howShortened={'1'} longInput={false} 
    checkRegex={regex} type={'password'} id={'password'} description={'Password:'} eDescription={error}/>;
    if (this.state.page == Page.LOGIN) {
      return (
        <div className='logInDiv'>
            <table border="1" className='logInTbl'>
              <tbody>
                <tr className='transperantBorder'>
                  <td className='gap'></td>
                  <td colSpan={'3'}>
                      <p className='logInHeader a0marginBtm'>Login</p>
                  </td>
                </tr>
                <tr className='uNameTr transperantBorder'>
                  <td>
                  </td>
                  <td className='uNameDescriptionTd'>
                    <span className="input-group-text spn" id="inputGroup-sizing-sm">User Name:</span>
                  </td>
                  <td className='uNameTd'  colSpan={'2'}>
                    {uNameElement}
                  </td>
                </tr>
                <tr className='transperantBorder'>
                  <td>
                  </td>
                  <td>
                    <span className="input-group-text spn" id="inputGroup-sizing-sm">Password:</span>
                  </td>
                  <td colSpan={'2'}>
                  {passElement}
                  </td>
                </tr>
                <tr className='errorTr transperantBorder'>
                  <td colSpan={'4'}>
                    <div className='errorTxt'>{this.state.error}</div>
                  </td>
                </tr>
                <tr className='transperantBorder'>
                  <td>
                  </td>
                  <td colSpan={'2'} className='vw12'>
                    <button
                    className="btn btn-primary" onClick={this.login} id="logIn" ><p className='pLogIn'>Login</p></button>
                  </td>
                  <td>
                    <Link to={'/register'}><img className='signImg' src={signImg}></img></Link>
                  </td>
                </tr>
              </tbody>
            </table>
        </div>
      );
    } else if (this.state.page == Page.LOADING) {
      return <div className="spinner-border" role="status " />
    } else if (this.state.page == Page.PASSED) {
      return <Navigate to="/webPage" />
    }
  }
    
}
export default Login;