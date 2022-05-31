import React, { Component } from 'react'
import '../bootstrap/dist/css/bootstrap.min.css';
import "./ChatGroups.css";
import defualtGroupImg from "../images/DefualtGroup.jpg";

class ChatGroup extends Component {
    constructor(props) {  
      super(props);
      this.state = {...props.group
        //   name: props.name , isClicked: props.isClicked,
        //   image: props.image , messages: props.messages 
      }
      this.setGroup = props.setGroup;
      this.handleClick = this.handleClick.bind(this);
    } 

    handleClick = ()=>{
      if(!this.state.isClicked){
        var oldGroup = this.state
        oldGroup.unreadMark = 0
        this.setGroup({groupIdToChange:oldGroup.id,newGroup:oldGroup,newClickedId:oldGroup.id})
      }
    }
    render() {
      //future logic to get the last message
      var lastmsg = "",lastmsgIcon=<></>;
      if(this.state.messages.length > 0){
        var last = this.state.messages[this.state.messages.length-1]
        if(last.Author!==this.props.userName){
          lastmsg+=last.Author+": "
        }
        if(last.type==="img"){
          lastmsg+="image"
          lastmsgIcon=<i className="bi bi-file-earmark-image-fill icn"></i>
        } else if(last.type==="audio"){
          lastmsg+="recording"
          lastmsgIcon=<i className="bi bi-soundwave icn"></i>
        } else{
          lastmsg+=last.Content
        }
        var lastDate = new Date(last.Created),now=new Date();
        var lastMsgTime = '';
        if(typeof last.Created !== 'undefined'){
          if(lastDate.toLocaleDateString()===now.toLocaleDateString()){
            //today
            var hours = lastDate.getHours()<10?"0"+lastDate.getHours():lastDate.getHours();
            var minutes = lastDate.getMinutes()<10?"0"+lastDate.getMinutes():lastDate.getMinutes();
            lastMsgTime = hours+":"+minutes;
          }
          else {
            lastDate.setHours(10,0,0);
            now.setHours(10,0,0);
            var elapsed = now.getTime()-lastDate.getTime();
            if(elapsed<(1000*3600*24+10000)&&elapsed>(1000*3600*24-10000)){
              lastMsgTime = 'Yesterday';
            }
            else{
              const monthNames = ["Jan. ", "Feb. ", "Mar. ", "Apr. ", "May. ", "Jun. ","Jul. ", "Aug. ", "Sep. ", "Oct. ", "Nov. r", "Dec. "];
              lastMsgTime = monthNames[lastDate.getMonth()]+lastDate.getDate()
            }
          }
        }
      }

      var unreadMsg = <></>;
      if(this.state.unreadMark>0){
        unreadMsg = <div className='unreadDiv'><p>{this.state.unreadMark}</p></div>
        if(this.state.unreadMark>99){
          unreadMsg = <div className='unreadDiv'><p>99+</p></div>
        }
      }
      var img;
      if(typeof this.state.image !== 'undefined'){
        img = <img className='img' src={this.state.image} alt="db error"></img>
      }else{
        img = <img className='img' src={defualtGroupImg} alt="db error"></img>
      }
      const linkClass = this.state.isClicked?"card group clickedgroup":"card group";
      return (
        <a className={linkClass} onClick={this.handleClick}>
          <table border = "0" className='bigtbl'>
            <tbody>
              <tr>
                <td className='imgTd'>
                {img}
                </td>
                <td className='textTd'>
                  <table border = "0" className='smalltbl'>
                    <tbody>
                      <tr className='headln'>
                        <td rowSpan={'2'}>
                          <p className="card-title groupname text-truncate">{this.state.name}</p>
                        </td>
                        <td colSpan={'2'}>
                          <p className='lastMsgTime'>{lastMsgTime}</p>
                            
                        </td>
                      </tr>
                      <tr className='middleTr'>
                        <td className='emptyTd'>
                          
                        </td>
                        <td className='unreadTd' rowSpan={'2'}>
                          {unreadMsg}
                        </td>
                      </tr>
                      <tr className='subtitles'>
                        <td  colSpan={'2'}>
                          <p className='card-subtitle lastmsg text-truncate'>{lastmsgIcon}{lastmsg}</p>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </td>
              </tr>
            </tbody>
          </table>
        </a>
      )
    }
  }
  export default ChatGroup