import React, { Component } from 'react'
import MessageList from './MessageList'
import MessageForm from './MessageForm'
import './Chat.css'
import defualtGroupImg from "../images/DefualtGroup.jpg";
import $ from 'jquery';
import serverUrl from '../ServerUrl';
import JWT from '../identification/Jwt';

class Chat extends Component {
    constructor(props) {
      super(props);
      this.state = {
          messages: props.givenChat,
          id: props.id
      }
      this.updateFunc = props.updateFunc;
    } 


    handleNewMessage = (text) => {
      var created = new Date();
      var changedGroup = null;
      var newMessage = {Content: text, Author:this.props.userName,Created: created, sentStatus: "loading"}
      if (this.props.unread === 0) {
        changedGroup = {messages:[...this.state.messages,newMessage]}
      } else {
        changedGroup = {messages:[...this.state.messages,newMessage], unread: this.props.unread + 1}
      }
      var allGroupsDiv = document.getElementById('groupsDivId');
      allGroupsDiv.scrollTop = 0;
      this.updateFunc({groupIdToChange:this.state.id,newGroup:changedGroup,groupIdToTop:this.state.id})

      $.ajax({
        url: serverUrl + "api/contacts/" + this.state.id + "/messages",
        type: 'POST',
        cache: false,
        contentType: "application/json",
        beforeSend: (xhr)=>{
          xhr.setRequestHeader('Authorization', 'Bearer ' + JWT.JWT);
        },
        data: JSON.stringify({
          "content": text
        }),
        success: () => {
          var message = changedGroup.messages.find((m) => { // get the good message
            return m.Created.toString() == created.toString();
          })
          message.sentStatus = "sent"; // change status to sent
          this.updateFunc({groupIdToChange:this.state.id,newGroup:changedGroup})
          $.ajax({
            url: this.props.server + "api/contacts/transfer",
            type: 'POST',
            cache: false,
            contentType: "application/json",
            data: JSON.stringify({
              "from": this.props.userName,
              "to": this.state.id,
              "content": text
            }),
            success: () => {
              var message = changedGroup.messages.find((m) => { // get the good message
                return m.Created.toString() == created.toString();
              })
              message.sentStatus = "recieved"; // change status
              this.updateFunc({groupIdToChange:this.state.id,newGroup:changedGroup})
            },
          })
        },
        error: () => {
          var message = changedGroup.messages.find((m) => { // get the failed message
            return m.Created.toString() == created.toString();
          })
          message.sentStatus = "error"; // change status to error
          this.updateFunc({groupIdToChange:this.state.id,newGroup:changedGroup})
        }
      })
    }
    render() {
      var img;
      if(typeof this.props.image !== 'undefined'){
        img = <img className='img' src={this.state.image} alt="db error"></img>
      }else{
        img = <img className='img' src={defualtGroupImg} alt="db error"></img>
      }
        return (
          <div className='chatContainer'>
            <div className='table-container'>
            <table border="0" className="chat-tbl">
                  <tbody>
                    <tr>
                      <td className="imgTd">
                        {img}
                      </td>
                      <td>
                        <div>
                          <p className="chat-name text-truncate">
                            {this.props.name}
                          </p>
                        </div>
                      </td>
                    </tr>
                  </tbody>
                </table>
                </div>
            <MessageList messages={this.state.messages} unread={this.props.unread} unreadOnTop={this.props.unreadOnTop} userName={this.props.userName}/>
            <MessageForm onMessageSend={this.handleNewMessage} />
          </div>
        )
    }
  }
  export default Chat