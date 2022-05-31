import React, { Component } from 'react'
import './MessageList.css'
import Message from './Message'
import $ from 'jquery';

class MessageList extends Component {
  
  constructor(props) {
    super(props);
    this.unreadMessageRef = React.createRef();
  }

  static defaultProps = {
    messages: [],
    unread: 0
  }

  scroll = () => {
    if (this.props.unreadOnTop && this.props.unread != 0) {
      this.unreadMessageRef.current.scrollIntoView();
    } else {
      $("#list").scrollTop($("#list")[0].scrollHeight);
    }
  }
  
  componentDidMount() {
    this.scroll();
  }
  
  componentDidUpdate() {
    this.scroll();
  }
  
  render() {
    const messages = this.props.messages.slice();
    const endToEndEnc = " end to end encryption";
    // messages.splice(0, 0, {type: 'text', body: endToEndEnc, icon: <i className="bi bi-lock-fill"></i>})
    messages.splice(0, 0, {Content: endToEndEnc, icon: <i className="bi bi-lock-fill"></i>})
    if (this.props.unread > 0) {
      var unreadMessage = this.props.unread>99 ? '99+ unread messages':this.props.unread + ' unread messages';
      var unreadIndex = Math.max(messages.length - this.props.unread, 1);
      // messages.splice(unreadIndex, 0, {type: 'text', body: unreadMessage})
      messages.splice(unreadIndex, 0, { Content: unreadMessage})
      const unreadMessageRef = this.unreadMessageRef;
      return (
        <div className="MessageList" id='list'>
          {messages.map(function(message, i) {
            if (i == unreadIndex) {
              return <div className='Wrapper' key={i} ref={unreadMessageRef}>
                      <Message {...message} me={(this.props.userName===message.Author)}/>
                    </div>
            }
            return <div className='Wrapper' key={i}>
              <Message {...message}  me={(this.props.userName===message.Author)}/>
            </div>
          }
            
          )}
        </div>
      )
    }
    return (
      <div className="MessageList" id='list'>
        {messages.map((message, i) => (
          <div className='Wrapper' key={i}>
            <Message {...message}  me={(this.props.userName===message.Author)}/>
          </div>
        ))}
      </div>
    )
  }
}

export default MessageList