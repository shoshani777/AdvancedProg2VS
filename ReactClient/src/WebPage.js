import React, { Component, useEffect } from "react";
import Chat from "./Chat/Chat";
import ChatGroup from "./ChatGroups/ChatGroups";
import chat_db from "./fictiveChatDB";
import './bootstrap/dist/css/bootstrap.min.css';
import NewContact from "./ChatGroups/NewContact";
import "./WebPage.css";
import DefualtChat from "./DefualtChat";
import serverUrl from "./ServerUrl";
import defualtImg from "./images/defualtChat.jpg";
import $ from 'jquery';
import * as signalR from '@microsoft/signalr';
import JWT from "./identification/Jwt";

class WebPage extends Component {
  constructor(props) {
    super(props);
    if (!props.userName) {
      window.location.href = "/logIn";
    }
    this.state = {
      groups:null,image:defualtImg,clickedId:null,userName:props.userName,unreadOnTop:false
    }
    this.ChangeStateFunc = this.ChangeState.bind(this);
    this.checkGroupNameFunc = this.checkGroupName.bind(this);
    this.checkContactNameFunc = this.checkContactName.bind(this);
    this.changeContactInDb = this.changeContactInDb.bind(this);
    // $.ajax({
    //   url: serverUrl+"api/flows",
    //   type: 'GET',
    //   cache: false,
    //   contentType: "application/json",
    //   beforeSend: (xhr)=>{
    //     xhr.setRequestHeader('Authorization', 'Bearer ' + JWT.JWT);
    //   },
    //   error: (error) => {
    //     console.log("error conecting to database")
    //   },
    //   success: (groups)=>{
    //     this.resetComponent(JSON.parse(groups))
    //   }
    // })
    this.getData();
    // this.getData().then(response => response.json()).then(groups=>this.resetComponent(groups));
    
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(serverUrl + "hubs/chat", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect()
      .build();
      this.connection.start()
            .then(() => {
                this.connection.on('ReceiveMessage', (message, userFor) => {
                  if(message!==null)
                  {
                    var actualMessage = {Id:message.id,Author:message.author,Content:message.content,Created:message.created,Chat:message.chat}
                  }
                  if (userFor !== this.props.userName) {
                    return;
                  }
                  if(message===null)
                  {
                    this.getData();
                    return;
                  }
                  for(let index = 0; index < this.state.groups.length; index++){
                    var group = this.state.groups[index]
                    if (group.id === actualMessage.Author) {
                      if (!group.isClicked) {
                        group.unreadMark = group.unreadMark + 1;
                      }

                      var changedGroup = null;
                  
                      if (group.unread === 0) {
                        changedGroup = {messages:[...group.messages,actualMessage]}
                      } else {
                        changedGroup = {messages:[...group.messages,actualMessage], unread: group.unread + 1}
                      }
                      this.ChangeStateFunc({groupIdToChange:group.id,newGroup: changedGroup,groupIdToTop:group.id})
                    }
                  }
                });
            })
            .catch(e => console.log('Connection failed: ', e));
  }

  async getData(){
    $.ajax({
      url: serverUrl+"api/flows",
      type: 'GET',
      cache: false,
      contentType: "application/json",
      beforeSend: (xhr)=>{
        xhr.setRequestHeader('Authorization', 'Bearer ' + JWT.JWT);
      },
      error: (error) => {
        console.log("error conecting to database")
      },
      success: (groups)=>{
        this.resetComponent(JSON.parse(groups))
      }
    })
  }

  async changeContactInDb({contactName,unread,unreadMark,isClicked}){
    //alert("contactName "+contactName+" unread "+unread+" unreadMark "+unreadMark+" isClicked "+isClicked)
    if(typeof contactName !== 'undefined') {
      $.ajax({
        url: serverUrl + "api/flows/contact/"+contactName,
        type: 'PUT',
        cache: false,
        contentType: "application/json",
        beforeSend: (xhr)=>{
          xhr.setRequestHeader('Authorization', 'Bearer ' + JWT.JWT);
        },
        data: JSON.stringify({
          "unread": typeof unread !== 'undefined'?unread:null
          ,
          "unreadMark": typeof unreadMark !== 'undefined'?unreadMark:null
          ,
          "isClicked": typeof isClicked !== 'undefined'?isClicked:null
        }),
        error: (error) => {
          console.log("error conecting to database")
        }
      })
    }
  }

  resetComponent(allGroups){
    for (let index = 0; index < allGroups.length; index++) {
      const element = allGroups[index];
      if(element.isClicked){
        allGroups[index].unread = 0;
        element.isClicked = false;
        this.changeContactInDb({contactName:allGroups[index].id,unread:0,isClicked:false})
      }
    }
    this.setState({groups:allGroups})
  }

  checkGroupName({nameToCheck}){
    for (let index = 0; index < this.state.groups.length; index++) {
      const element = this.state.groups[index];
      if(element.name===nameToCheck){
        return false;
      }
    }
    return true;
  }

  checkContactName({nameToCheck}){
    for (let index = 0; index < this.state.groups.length; index++) {
      const element = this.state.groups[index];
      if(element.id===nameToCheck){
        return false;
      }
    }
    return true;
  }

  async ChangeState({groupIdToChange,newGroup,newClickedId,groupIdToTop}){
    var newState = this.state
    newState.unreadOnTop = false
    if(typeof groupIdToTop !== 'undefined'){
      var groupToTop = null
      for (let index = 0; index < newState.groups.length; index++) {
        const element = newState.groups[index];
        if(element.id === groupIdToTop){
          groupToTop = element
          newState.groups.splice(index,1)
        }
      }
      newState.groups.unshift(groupToTop)
    }
    if(typeof groupIdToChange !== 'undefined'){
      var gruopPlace = null
      for (let index = 0; index < newState.groups.length; index++) {
        const element = newState.groups[index];
        if(element.id === groupIdToChange){
          gruopPlace = index
        }
      }
      newState.groups[gruopPlace] = {...newState.groups[gruopPlace],...newGroup}
    } else if(typeof newGroup !== 'undefined'){
      newState.groups.unshift(newGroup);
      newState.clickedId = newGroup.id;
      for (let index = 0; index < newState.groups.length; index++) {
        const element = newState.groups[index];
        if(element.isClicked){
          newState.groups[index].unread = 0;
          this.changeContactInDb({contactName:newState.groups[index].id,unread:0,isClicked:false})
        }
        if(element.id === newGroup.id){
          this.changeContactInDb({contactName:newState.groups[index].id,isClicked:true,unreadMark:0})
        }
        newState.groups[index].isClicked = (element.id === newGroup.id);
      }
    }
    if(typeof newClickedId !== 'undefined'){
      newState.clickedId = newClickedId
      for (let index = 0; index < newState.groups.length; index++) {
        const element = newState.groups[index];
        if(element.isClicked){
          newState.groups[index].unread = 0;
          this.changeContactInDb({contactName:newState.groups[index].id,unread:0,isClicked:false})
        }
        if(element.id===newClickedId){
          this.changeContactInDb({contactName:newState.groups[index].id,isClicked:true,unreadMark:0})
          newState.unreadOnTop=true
        }
        newState.groups[index].isClicked = (element.id === newClickedId);
      }
    }
    this.setState({...newState });
  }

  render() {
    if(this.state.groups==null){return (<></>);}
    var list =[]
    for (let index = 0; index < this.state.groups.length; index++) {
      list.push(<li className="list-group-item noMargin" key={Math.random()}><ChatGroup group={{...this.state.groups[index]}} userName={this.props.userName} setGroup={this.ChangeStateFunc}/></li>);
    }
    // var chat=null;
    var chat = <DefualtChat />;//need defualt chat
    if(this.state.clickedId!==null){
      for (let index = 0; index < this.state.groups.length; index++) {
        const element = this.state.groups[index];
        if(element.id===this.state.clickedId){
          chat = <>
          <Chat key={Math.random()} givenChat={element.messages} unread={element.unread} id={this.state.groups[index].id}
          server={element.server} updateFunc={this.ChangeStateFunc} name={element.name} unreadOnTop={this.state.unreadOnTop} userName={this.state.userName}/>
          </>
        }
      }
    }
    
    return (
      <>
      <div>
        <div className="webPageContainer container-fluid">
          <div className="row">
            <div className="col-1"></div>
            <div className="col-2 col-md-auto groupColumn overflow-auto">
              <div>
                <table border="0" className="userTbl">
                  <tbody>
                    <tr>
                      <td className="imgTd">
                        <img className="userImg" alt={"couldn't load"} src={this.state.image}/>
                      </td>
                      <td>
                        <div>
                          <p className="userNameTxt text-truncate">
                            {this.state.userName}
                          </p>
                        </div>
                      </td>
                      <td className="newContactTd">
                        <NewContact AddingFunc={this.ChangeStateFunc} checkGroupName={this.checkGroupNameFunc} checkContactName={this.checkContactNameFunc} userName={this.state.userName}/>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div id="groupsDivId" className="groupDiv">
                <ul className="list-group">
                  {list}
                </ul>
              </div>
            </div>
            <div className="col-8 Chat">
              {chat}
            </div>
            <div className="col-1 overflow-auto">
            </div>
          </div>
        </div>
      </div>
      </>
    )
  }
}
export default WebPage