import Input from './Input';
import React from "react";
import {
  Link,
} from "react-router-dom";
import users from './users';
import nameToLink from '../nameToLink';
import defualtImg from "../images/defualtChat.jpg";
import $ from 'jquery';
import chat_db from '../fictiveChatDB';
import signImg from '../images/SignIn.png';
import './Register.css';
import { Navigate } from "react-router-dom";
import serverUrl from '../ServerUrl';
import JWT from './Jwt';

const Page = {
  REGISTER: 0,
  LOADING: 1,
  PASSED: 2
}
class Register extends React.Component {

  constructor(props) {
    super(props)
    this.state = {
      userName : 0, nickName : 0, password : 0, confirmPass : 0, error:'', page: Page.REGISTER
    };
    this.cPassRef = React.createRef();
    this.setUserName = this.setUserName.bind(this);
    this.setNickName = this.setNickName.bind(this);
    this.setPassword = this.setPassword.bind(this);
    this.setConfirmPass = this.setConfirmPass.bind(this);
    this.register = this.register.bind(this);  
    this.setPage = props.setPage;
    this.setUser = props.setUser;
  }

  async register() {
    if (!(this.state.userName && this.state.nickName && this.state.password &&
      this.state.confirmPass)){
        this.setState({error:'Please fill out the required information'});
        return;
    }
    this.setState({page: Page.LOADING});
    const uName = this.state.userName;
    $.ajax({
      url: serverUrl + "api/Users/register",
      type: 'POST',
      cache: false,
      contentType: "application/json",
      data: JSON.stringify({
        "userName": uName,
        "password": this.state.password,
        "nickName": this.state.nickName
      }),
      success: (data) => { // the JWT is in data
        JWT.JWT = data;
        this.setUser(uName);
        this.setPage(nameToLink.get('webPage'));
        this.setState({page: Page.PASSED});
      },
      error: (error) => {
        this.setState({userName : 0, nickName : 0, password : 0, confirmPass : 0, error: error.responseText, page: Page.REGISTER});
      }
    })
  }

  setUserName(value) {
    this.setState({
      userName: value
    })
  }
  setNickName(value) {
    this.setState({
      nickName: value
    })
  }
  setPassword(value) {
    this.setState({
      password: value
    })
  }
  setConfirmPass(value) {
    this.setState({
      confirmPass: value
    })
  }

  render() {
    const regex = '^[a-zA-Z]';
    const error = 'must begin with a letter'
    const confirmPassError = 'must match password'


    const uNameElement = <Input inputSetter={this.setUserName} checkRegex={regex} type={'text'} howShortened={'2'} longInput={false}
    id={'uname'} description={'User Name'} eDescription={error}/>;
    const nNameElement = <Input inputSetter={this.setNickName} checkRegex={regex} type={'text'} howShortened={'2'} longInput={false}
    id={'nname'} description={'Nick Name'} eDescription={error}/>;
    const cPassElement = <Input inputSetter={this.setConfirmPass} checkRegex={'^' + this.state.password + '$'} type={'password'} longInput={false}
    id={'cpass'} description={'Confirm Password'} eDescription={confirmPassError} ref={this.cPassRef} howShortened={'2'}/>;
    const passElement = <Input inputSetter={this.setPassword} cSetter={this.setConfirmPass} cPassError={confirmPassError} longInput={false}
    checkRegex={regex} type={'password'} id={'pass'} description={'Password'} eDescription={error} howShortened={'2'}
    cPassRef={this.cPassRef}/>;
    if (this.state.page == Page.REGISTER) {
      return ( 
        <div className='container'>
            <div className='logInDiv'>
              <table border="1" className='logInTbl'>
                <tbody>
                  <tr className='transperantBorderReg trHead'>
                    <td className='gap'></td>
                    <td colSpan={'3'}>
                        <p className='logInHeader a0marginBtm'>Register</p>
                    </td>
                    <td className='gap'></td>
                  </tr>
                  <tr className='TrReg transperantBorderReg'>
                    <td>
                    </td>
                    <td className='uNameDescriptionTd'>
                      <span className="input-group-text spn" id="inputGroup-sizing-sm">
                        <p className='a0marginBtm uNameP'>User Name:</p>
                      </span>
                    </td>
                    <td className='uNameTd'  colSpan={'2'}>
                      {uNameElement}
                    </td>
                  </tr>
                  <tr className='TrReg transperantBorderReg'>
                    <td>
                    </td>
                    <td className='nNameDescriptionTd'>
                      <span className="input-group-text spn" id="inputGroup-sizing-sm">
                        <p className='a0marginBtm uNameP'>Nick Name:</p>
                      </span>
                    </td>
                    <td className='nNameTd'  colSpan={'2'}>
                      {nNameElement}
                    </td>
                  </tr>
                  <tr className='TrReg transperantBorderReg'>
                    <td>
                    </td>
                    <td>
                      <span className="input-group-text spn" id="inputGroup-sizing-sm">
                        <p className='a0marginBtm paswordP'>Password:</p>
                      </span>
                    </td>
                    <td colSpan={'2'}>
                    {passElement}
                    </td>
                  </tr>
                  <tr className='TrReg transperantBorderReg'>
                    <td>
                    </td>
                    <td>
                      <span className="input-group-text spn" id="inputGroup-sizing-sm">Confirm Password:</span>
                    </td>
                    <td colSpan={'2'}>
                    {cPassElement}
                    </td>
                  </tr>
                  <tr className='errorTr transperantBorderReg'>
                    <td colSpan={'4'}>
                      <div id='errorReg' className='errorTxt'>{this.state.error}</div>
                    </td>
                  </tr>
                  <tr className='transperantBorderReg'>
                    <td>
                    </td>
                    <td colSpan={'2'} className='vw12'>
                      <button
                        className="btn btn-primary" id="register" onClick={this.register} ><p className='pRegister'>Register</p></button>
                    </td>
                    <td>
                      <Link to={'/logIn'}><img className='signImgReg' src={signImg}></img></Link>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
        </div>
      );
    } else if (this.state.page == Page.LOADING) {
      return <div className="spinner-border" role="status " />
    } else if (this.state.page == Page.PASSED) {
      return <Navigate to="/webPage" />
    }
  }
}
export default Register;