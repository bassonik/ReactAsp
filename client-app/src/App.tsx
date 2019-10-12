import React, { Component } from 'react';
import './App.css';
import axios from 'axios';
import { Header, Icon, List } from 'semantic-ui-react';

class App extends Component {
  state = {
    values: []
  }

  async componentDidMount() {
    let result = await axios.get('http://localhost:5000/api/values');
    this.setState({
      values: result.data
    })
  }

  render() {
    return (
      <div>
        <Header as='h2'>
          <Icon name='blind' />
          <Header.Content>Users</Header.Content>
        </Header>
        <List>
          {this.state.values.map((val: any) => {
            return <List.Item key={val.id}>{val.name}</List.Item>
          })}
        </List>
      </div>
    );
  }
}

export default App;
