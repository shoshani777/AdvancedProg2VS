import oriImg from "./images/ori.jpg";
import giladImg from "./images/gilad.jpg";
import bobImg from "./images/bob.jpg";
import aliceImg from "./images/alice.jpg";
import hemiImg from "./images/hemi.png";
import rickRoll from './images/rickRoll.mp3';
import longCoolImg from "./images/LongCoolImage"; 
import long100Img from "./images/Long100Image";
import bellCurveImg from "./images/bellCurve.png";
import booksImg from "./images/books.jpg";


const chat_db = new Map([
    ["shoshani777",
        { image:oriImg , groups:
            [
                {id:1 , isClicked: false, name:"gilad" , image:giladImg ,isgroup:false, unreadMark: 4, unread: 4, messages:
                    [
                        {me: true, body: 'hello to gilad from ori', type: 'text',author:'me',date:new Date("April 28, 2022 00:01:30")},
                        {me: false, body: 'thank you ori, I think we did a great job', type: 'text',author:'gilad',date:new Date("April 28, 2022 00:01:50")},
                        {me: true, body: 'I agree', type: 'text',author:'me',date:new Date("April 28, 2022 00:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'gilad',date:new Date("April 28, 2022 00:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'gilad',date:new Date("April 28, 2022 00:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me',date:new Date("April 28, 2022 00:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 28, 2022 00:03:30")}
                    ]
                },
                {id:2 , isClicked: false, name:"bob" , image:bobImg ,isgroup:false, unreadMark: 0, unread: 0, messages:
                    [
                        {me: true, body: 'hello to bob from ori', type: 'text',author:'me', date:new Date("April 27, 2022 23:01:30")},
                        {me: false, body: 'thank you ori, I love what you did with the place', type: 'text',author:'bob',date:new Date("April 27, 2022 23:01:50")},
                        {me: true, body: 'I agree', type: 'text',author:'me',date:new Date("April 27, 2022 23:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'bob',date:new Date("April 27, 2022 23:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'bob',date:new Date("April 27, 2022 23:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me',date:new Date("April 27, 2022 23:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 27, 2022 23 00:03:30")}
                    ]
                },
                {id:3 , isClicked: false, name:"alice" , image:aliceImg ,isgroup:false, unreadMark: 7, unread: 7, messages:
                    [
                        {me: true, body: 'hello to alice from ori', type: 'text',author:'me', date:new Date("April 27, 2022 20:01:30")},
                        {me: false, body: 'thank you ori, I love what you did with the place', type: 'text',author:'alice', date:new Date("April 27, 2022 20:01:50")},
                        {me: true, body: 'thanks', type: 'text',author:'me', date:new Date("April 27, 2022 20:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'alice', date:new Date("April 27, 2022 20:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'alice', date:new Date("April 27, 2022 20:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me', date:new Date("April 27, 2022 20:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 27, 2022 20:03:30")}
                    ]
                },
                {id:4 , isClicked: false, name:"hemi" , image:hemiImg ,isgroup:false, unreadMark: 8, unread: 8, messages:
                    [
                        {me: true, body: 'hemi please give me a good grade', type: 'text',author:'me', date:new Date("April 26, 2022 20:01:30")},
                        {me: false, body: 'no problem, this is your grade', type: 'text',author:'hemi', date:new Date("April 26, 2022 20:01:50")},
                        {me: false, body: rickRoll, type: 'audio',author:'hemi', date:new Date("April 26, 2022 20:02:10")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me', date:new Date("April 26, 2022 20:02:27")},
                        {me: true, body: longCoolImg, type: 'img',author:'me', date:new Date("April 26, 2022 20:02:35")},
                        {me: true, body: "wait... so what is my grade?", type: 'text',author:'me', date:new Date("April 26, 2022 20:02:50")},
                        {me: false, body: 'okay, okay, this is your grade', type: 'text',author:'hemi', date:new Date("April 26, 2022 20:03:10")},
                        {me: false, body: rickRoll, type: 'audio',author:'hemi', date:new Date("April 26, 2022 20:03:30")}
                    ]
                },
                {id:5 , isClicked: false, name:"grading students" , image:bellCurveImg ,isgroup:true, unreadMark: 9, unread: 9, messages:
                    [
                        {me: true, body: "hello everyone, we have to decide on gilad's and ori's score", type: 'text',author:'me', date:new Date("April 25, 2022 18:30:00")},
                        {me: false, body: 'I say we fail them', type: 'text',author:'uriel', date:new Date("April 25, 2022 18:41:30")},
                        {me: false, body: 'do you really mean that?', type: 'text',author:'hemi', date:new Date("April 25, 18:41:50")},
                        {me: false, body: rickRoll, type: 'audio',author:'uriel', date:new Date("April 25, 2022 18:42:07")},
                        {me: false, body: 'oh, you were just kidding', type: 'text',author:'hemi', date:new Date("April 25, 2022 18:43:00")},
                        {me: false, body: 'yes, but I know just how much we should give them!', type: 'text',author:'uriel', date:new Date("April 25, 2022 19:01:00")},
                        {me: false, body: long100Img, type: 'img',author:'uriel', date:new Date("April 25, 2022 19:01:30")},
                        {me: false, body: 'correct!', type: 'text',author:'hemi', date:new Date("April 25, 2022 19:21:30")},
                        {me: true, body: 'good talk guys!', type: 'text',author:'me',date:new Date("April 25, 2022 20:01:30")}
                    ]
                }
            ]
        }
    ],
    ["gilad517",
        { image:giladImg , groups:
            [
                {id:1 , isClicked: false, name:"ori" , image:oriImg ,isgroup:false, unreadMark: 4, unread: 4, messages:
                    [
                        {me: true, body: 'hello to ori from gilad', type: 'text',author:'me',date:new Date("April 28, 2022 00:01:30")},
                        {me: false, body: 'thank you gilad, I think we did a great job', type: 'text',author:'ori',date:new Date("April 28, 2022 00:01:50")},
                        {me: true, body: 'I agree', type: 'text',author:'me',date:new Date("April 28, 2022 00:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'ori',date:new Date("April 28, 2022 00:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'ori',date:new Date("April 28, 2022 00:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me',date:new Date("April 28, 2022 00:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 28, 2022 00:03:30")}
                    ]
                },
                {id:2 , isClicked: false, name:"bob" , image:bobImg ,isgroup:false, unreadMark: 0, unread: 0, messages:
                    [
                        {me: true, body: 'hello to bob from gilad', type: 'text',author:'me', date:new Date("April 27, 2022 23:01:30")},
                        {me: false, body: 'thank you gilad, I love what you did with the place', type: 'text',author:'bob',date:new Date("April 27, 2022 23:01:50")},
                        {me: true, body: 'thanks', type: 'text',author:'me',date:new Date("April 27, 2022 23:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'bob',date:new Date("April 27, 2022 23:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'bob',date:new Date("April 27, 2022 23:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me',date:new Date("April 27, 2022 23:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 27, 2022 23 00:03:30")}
                    ]
                },
                {id:3 , isClicked: false, name:"alice" , image:aliceImg ,isgroup:false, unreadMark: 7, unread: 7, messages:
                    [
                        {me: true, body: 'hello to alice from gilad', type: 'text',author:'me', date:new Date("April 27, 2022 20:01:30")},
                        {me: false, body: 'thank you gilad, I love what you did with the place', type: 'text',author:'alice', date:new Date("April 27, 2022 20:01:50")},
                        {me: true, body: 'thanks', type: 'text',author:'me', date:new Date("April 27, 2022 20:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'alice', date:new Date("April 27, 2022 20:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'alice', date:new Date("April 27, 2022 20:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me', date:new Date("April 27, 2022 20:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 27, 2022 20:03:30")}
                    ]
                },
                {id:4 , isClicked: false, name:"hemi" , image:hemiImg ,isgroup:false, unreadMark: 8, unread: 8, messages:
                    [
                        {me: true, body: 'hemi please give me a good grade', type: 'text',author:'me', date:new Date("April 26, 2022 20:01:30")},
                        {me: false, body: 'no problem, this is your grade', type: 'text',author:'hemi', date:new Date("April 26, 2022 20:01:50")},
                        {me: false, body: rickRoll, type: 'audio',author:'hemi', date:new Date("April 26, 2022 20:02:10")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me', date:new Date("April 26, 2022 20:02:27")},
                        {me: true, body: longCoolImg, type: 'img',author:'me', date:new Date("April 26, 2022 20:02:35")},
                        {me: true, body: "wait... so what is my grade?", type: 'text',author:'me', date:new Date("April 26, 2022 20:02:50")},
                        {me: false, body: 'okay, okay, this is your grade', type: 'text',author:'hemi', date:new Date("April 26, 2022 20:03:10")},
                        {me: false, body: rickRoll, type: 'audio',author:'hemi', date:new Date("April 26, 2022 20:03:30")}
                    ]
                },
                {id:5 , isClicked: false, name:"grading students" , image:bellCurveImg ,isgroup:true, unreadMark: 9, unread: 9, messages:
                    [
                        {me: true, body: "hello everyone, we have to decide on gilad's and ori's score", type: 'text',author:'me', date:new Date("April 25, 2022 18:30:00")},
                        {me: false, body: 'I say we fail them', type: 'text',author:'uriel', date:new Date("April 25, 2022 18:41:30")},
                        {me: false, body: 'do you really mean that?', type: 'text',author:'hemi', date:new Date("April 25, 18:41:50")},
                        {me: false, body: rickRoll, type: 'audio',author:'uriel', date:new Date("April 25, 2022 18:42:07")},
                        {me: false, body: 'oh, you were just kidding', type: 'text',author:'hemi', date:new Date("April 25, 2022 18:43:00")},
                        {me: false, body: 'yes, but I know just how much we should give them!', type: 'text',author:'uriel', date:new Date("April 25, 2022 19:01:00")},
                        {me: false, body: long100Img, type: 'img',author:'uriel', date:new Date("April 25, 2022 19:01:30")},
                        {me: false, body: 'correct!', type: 'text',author:'hemi', date:new Date("April 25, 2022 19:21:30")},
                        {me: true, body: 'good talk guys!', type: 'text',author:'me',date:new Date("April 25, 2022 20:01:30")}
                    ]
                }
            ]
        }
    ],
    ["student1024",
        { image:booksImg , groups:
            [
                {id:1 , isClicked: false, name:"gilad" , image:giladImg ,isgroup:false, unreadMark: 4, unread: 4, messages:
                    [
                        {me: true, body: 'hello to gilad from student', type: 'text',author:'me',date:new Date("April 28, 2022 00:01:30")},
                        {me: false, body: 'thank you student, I think we did a great job', type: 'text',author:'gilad',date:new Date("April 28, 2022 00:01:50")},
                        {me: true, body: 'I agree', type: 'text',author:'me',date:new Date("April 28, 2022 00:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'gilad',date:new Date("April 28, 2022 00:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'gilad',date:new Date("April 28, 2022 00:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me',date:new Date("April 28, 2022 00:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 28, 2022 00:03:30")}
                    ]
                },
                {id:2 , isClicked: false, name:"ori" , image:oriImg ,isgroup:false, unreadMark: 4, unread: 4, messages:
                    [
                        {me: true, body: 'hello to ori from student', type: 'text',author:'me', date:new Date("April 27, 2022 23:01:30")},
                        {me: false, body: 'thank you student, I think we did a great job', type: 'text',author:'ori',date:new Date("April 27, 2022 23:01:50")},
                        {me: true, body: 'I agree', type: 'text',author:'me',date:new Date("April 27, 2022 23:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'ori',date:new Date("April 27, 2022 23:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'ori',date:new Date("April 27, 2022 23:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me',date:new Date("April 27, 2022 23:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 27, 2022 23 00:03:30")}
                    ]
                },
                {id:3 , isClicked: false, name:"bob" , image:bobImg ,isgroup:false, unreadMark: 0, unread: 0, messages:
                    [
                        {me: true, body: 'hello to bob from student', type: 'text',author:'me', date:new Date("April 27, 2022 20:01:30")},
                        {me: false, body: 'thank you student, I love what you did with the place', type: 'text',author:'bob', date:new Date("April 27, 2022 20:01:50")},
                        {me: true, body: 'thanks', type: 'text',author:'me', date:new Date("April 27, 2022 20:02:10")},
                        {me: false, body: 'Look! The new lecture was just uploaded to moodle in mp3 format!', type: 'text',author:'bob', date:new Date("April 27, 2022 20:02:30")},
                        {me: false, body: rickRoll, type: 'audio',author:'bob', date:new Date("April 27, 2022 20:02:50")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me', date:new Date("April 27, 2022 20:03:10")},
                        {me: true, body: longCoolImg, type: 'img',author:'me',date:new Date("April 27, 2022 20:03:30")}
                    ]
                },
                {id:4 , isClicked: false, name:"hemi" , image:hemiImg ,isgroup:false, unreadMark: 8, unread: 8, messages:
                    [
                        {me: true, body: 'hemi please give me them good grade', type: 'text',author:'me', date:new Date("April 26, 2022 20:01:30")},
                        {me: false, body: 'no problem, this is their grade', type: 'text',author:'hemi', date:new Date("April 26, 2022 20:01:50")},
                        {me: false, body: rickRoll, type: 'audio',author:'hemi', date:new Date("April 26, 2022 20:02:10")},
                        {me: true, body: "You rick rolled me!", type: 'text',author:'me', date:new Date("April 26, 2022 20:02:27")},
                        {me: true, body: longCoolImg, type: 'img',author:'me', date:new Date("April 26, 2022 20:02:35")},
                        {me: true, body: "wait... so what is their grade?", type: 'text',author:'me', date:new Date("April 26, 2022 20:02:50")},
                        {me: false, body: 'okay, okay, this is their grade', type: 'text',author:'hemi', date:new Date("April 26, 2022 20:03:10")},
                        {me: false, body: rickRoll, type: 'audio',author:'hemi', date:new Date("April 26, 2022 20:03:30")}
                    ]
                },
                {id:5 , isClicked: false, name:"grading students" , image:bellCurveImg ,isgroup:true, unreadMark: 9, unread: 9, messages:
                    [
                        {me: true, body: "hello everyone, we have to decide on gilad's and ori's score", type: 'text',author:'me', date:new Date("April 25, 2022 18:30:00")},
                        {me: false, body: 'I say we fail them', type: 'text',author:'uriel', date:new Date("April 25, 2022 18:41:30")},
                        {me: false, body: 'do you really mean that?', type: 'text',author:'hemi', date:new Date("April 25, 18:41:50")},
                        {me: false, body: rickRoll, type: 'audio',author:'uriel', date:new Date("April 25, 2022 18:42:07")},
                        {me: false, body: 'oh, you were just kidding', type: 'text',author:'hemi', date:new Date("April 25, 2022 18:43:00")},
                        {me: false, body: 'yes, but I know just how much we should give them!', type: 'text',author:'uriel', date:new Date("April 25, 2022 19:01:00")},
                        {me: false, body: long100Img, type: 'img',author:'uriel', date:new Date("April 25, 2022 19:01:30")},
                        {me: false, body: 'correct!', type: 'text',author:'hemi', date:new Date("April 25, 2022 19:21:30")},
                        {me: true, body: 'good talk guys!, they deserve it', type: 'text',author:'me',date:new Date("April 25, 2022 20:01:30")}
                    ]
                }
            ]
        }
    ]
]);

export default chat_db;