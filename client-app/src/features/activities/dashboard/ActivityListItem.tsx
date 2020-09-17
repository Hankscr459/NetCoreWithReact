import React from 'react'
// import ActivityStore from '../../../app/stores/activityStore'
import { Button, Icon, Item,  Segment } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { IActivity } from '../../../app/models/activity';
import { format } from 'date-fns'

const ActivityListItem: React.FC<{activity: IActivity}> = ({activity}) => {
    // const activityStore = useContext(ActivityStore)
    // const {
    //     deleteActivity, 
    //     submitting, 
    //     target
    // } = activityStore
    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item key={activity.id}>
                        <Item.Image size='tiny' circular src='/assets/user.png' />
                        <Item.Content>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Description>
                                Hosted by Bob
                            </Item.Description>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <Icon name='clock' />{format(activity.date, 'h:mm a')}
                <Icon name='marker' />{activity.venue}, {activity.city}
            </Segment>
            <Segment secondary>
                Attendes will go here
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button 
                    as={Link} to={`/activities/${activity.id}`}
                    floated='right' 
                    content='View' 
                    color='blue' 
                />
            </Segment>
        </Segment.Group>
    )
}

export default ActivityListItem

/* <Button
    target={target}
    name={activity.id}
    loading={target === activity.id && submitting}
    onClick={(e) => deleteActivity(e, activity.id)}
    floated='right' 
    content='Delete' 
    color='red' 
/> */