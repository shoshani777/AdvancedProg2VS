import React, { Component } from 'react'
import './Message.css'
import peopleToColor from './peopleToColor';

class Message extends Component {
  
  // randomColor = () => {
  //   var result = Math.floor(Math.random() * 16); // random between 0-15 (include)
  //   for (var i = 0; i < 5; i++) {
  //     result = result << 4;
  //     result += Math.floor(Math.random() * 16);
  //   } // result now contains a random color
  //   const colorWithoutGreen = parseInt('FF00FF', 16);
  //   result = result & colorWithoutGreen; // remove the green color from result
  //   return result.toString(16);
  // }

  render() {
    var messageClass = 'Message';
    messageClass += this.props.Author ? '' : ' log';
    if (this.props.Author) {
      messageClass += this.props.me? ' me' : ' other';
    }
    var loading = <i class="bi bi-clock"></i>
    var check = <i class="bi bi-check2"></i>
    var checkAll = <i class="bi bi-check2-all"></i>
    var status = null;
    if(this.props.me){
      if(typeof this.props.sentStatus ==='undefined')
      {
        status = checkAll;
      }
      if(this.props.sentStatus==='loading')
      {
        status = loading;
      } else if(this.props.sentStatus==='sent')
      {
        status = check;
      } else if(this.props.sentStatus==='recieved')
      {
        status = checkAll;
      }
    }
    return (
      <div className={messageClass}>
        {this.props.icon}
        {this.props.Content}
        <div className='statusCs'>
          {status}
        </div>
      </div>
    )
  }
}

export default Message