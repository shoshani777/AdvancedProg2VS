import React, { Component } from 'react';
import '../bootstrap/dist/css/bootstrap.min.css';
import "./NewContact.css";
import defualtImg from "../images/DefualtGroup.jpg";
import Input from '../identification/Input';
import $ from 'jquery';
import serverUrl from "../ServerUrl";
import JWT from '../identification/Jwt';

class NewContact extends Component {
    constructor(props) {
      super(props);
      this.state={
        contactName:'',chatName:'',contactServer:'',error:'',image:defualtImg
      }
      this.addFunc = props.AddingFunc;
      this.setChatNameFunc = this.setChatName.bind(this);
      this.setContactNameFunc = this.setContactName.bind(this);
      this.setContactServerFunc = this.setContactServer.bind(this);
      this.checkGroupNameFunc = props.checkGroupName;
      this.checkContactName = props.checkContactName;
      this.inputRef1 = React.createRef();
      this.inputRef2 = React.createRef();
      this.inputRef3 = React.createRef();
    }

    resetNameFunc=()=>{
        this.setState({
            Image: defualtImg,error:''
        });
        this.setChatNameFunc("");
        this.setContactName("");
        this.setContactServer("");
        this.inputRef1.current.setState({
            inputClass: '',
            error: ''
        });
        this.inputRef2.current.setState({
            inputClass: '',
            error: ''
        });
        this.inputRef3.current.setState({
            inputClass: '',
            error: ''
        })
        $("#ChatNameId").val('');
        $("#ContactNameId").val('');
        $("#ContactServerId").val('');
        
    }
    setChatName(value) {
        this.setState({
            chatName: value
        })
    }
    setContactName(value) {
        this.setState({
            contactName: value
        })
    }
    setContactServer(value) {
        this.setState({
            contactServer: value
        })
    }
    handleClickFunc=async ()=>{
        const input1 = document.getElementById('ChatNameId')
        const input2 = document.getElementById('ContactNameId')
        const input3 = document.getElementById('ContactServerId')
        if(this.state.chatName===''||this.state.contactName===''||this.state.contactServer===''){
            this.setState({error: "Please enter a chat name, user name, and server"});
            return;
        }
        if(this.state.chatName===null||this.state.contactName===null||this.state.contactServer===null){
            this.setState({error: "Please enter a chat name, user name, and server"});
            return;
        }
        if(!this.checkGroupNameFunc({nameToCheck:this.state.chatName})){
            this.setState({error: "Group name already taken"});
            return;
        }
        if(!this.checkContactName({nameToCheck:this.state.contactName})){
            this.setState({error: "Already has a chat with this contact"});
            return;
        }
        if(input1.classList.contains("is-valid")&&input2.classList.contains("is-valid")&&input3.classList.contains("is-valid")){
                this.setState({error: ""});
                $.ajax({
                    url: serverUrl + "api/contacts/",
                    type: 'POST',
                    cache: false,
                    contentType: "application/json",
                    beforeSend: (xhr)=>{
                      xhr.setRequestHeader('Authorization', 'Bearer ' + JWT.JWT);
                    },
                    data: JSON.stringify({
                      "id": this.state.contactName,
                      "name": this.state.chatName,
                      "server": this.state.contactServer
                    }),
                    success: async () => {
                      $.ajax({
                        url: this.state.contactServer + "api/contacts/invitations",
                        type: 'POST',
                        cache: false,
                        contentType: "application/json",
                        data: JSON.stringify({
                          "from": this.props.userName,
                          "to": this.state.contactName,
                          "server": serverUrl
                        }),
                        success:async ()=>{
                            this.addFunc({newGroup:{id: this.state.contactName, isClicked: false, name:this.state.chatName,
                                      unreadMark: 0, unread: 0, messages:[],last:null,lastdate:new Date(),server:this.state.contactServer}})
                            $("[data-dismiss=modal]").trigger({ type: "click" });
                        }
                      })
                    }
                  })


        }
    }
    
    
    resetErr=()=>{
        this.setState({error:''})
    }

    render() {
        const regex = '^[a-zA-Z]';
        const error = 'must begin with a letter';
        const ChatNameElement = <Input inputSetter={this.setChatNameFunc} checkRegex={regex} type={'text'} howShortened={'3'} longInput={true} 
        id={'ChatNameId'} description={'Chat Name'} ref={this.inputRef1} eDescription={error} placeholder={'Chat Name'}/>;
        const ContactNameElement = <Input inputSetter={this.setContactNameFunc} checkRegex={regex} type={'text'} howShortened={'3'} longInput={true} 
        id={'ContactNameId'} description={'Contact Name'} ref={this.inputRef2} eDescription={error} placeholder={'User Name'}/>;
        const ContactServerElement = <Input inputSetter={this.setContactServerFunc} checkRegex={regex} type={'text'} howShortened={'3'} longInput={true} 
        id={'ContactServerId'} description={'Contact Server Name'} ref={this.inputRef3} eDescription={error} placeholder={'Server'}/>;
        return (
          <>
            <a type="button" href='#' className="btn addChat" data-toggle="modal" data-target="#CenteralModal" onClick={this.resetNameFunc}>
                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-person-plus addChatIcon" viewBox="0 0 16 16">
                    <path d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0zm4 8c0 1-1 1-1 1H1s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C9.516 10.68 8.289 10 6 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z"/>
                    <path fillRule="evenodd" d="M13.5 5a.5.5 0 0 1 .5.5V7h1.5a.5.5 0 0 1 0 1H14v1.5a.5.5 0 0 1-1 0V8h-1.5a.5.5 0 0 1 0-1H13V5.5a.5.5 0 0 1 .5-.5z"/>
                </svg>
            </a>

            <div className="modal fade" id="CenteralModal" tabIndex="-1" role="dialog" aria-labelledby="CentralModalTitle" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered" role="document">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title" id="exampleModalLongTitle">Add a new Chat</h5>
                    <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div className="modal-body">
                    {ChatNameElement}
                    {ContactNameElement}
                    {ContactServerElement}
                    <div className='errorTxt errorTxtDivAtModal'>{this.state.error}</div>
                </div>
                <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" data-dismiss="modal" onClick={this.resetErr}>Close</button>
                    <button type="button" className="btn btn-outline-success" onClick={this.handleClickFunc}>Add</button>
                </div>
                </div>
            </div>
            </div>
          </>
      )
    }
  }
  export default NewContact