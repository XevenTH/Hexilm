import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import Browse from '../../Components/Movies/Browse/Browse';
import Dashboard from '../../Components/Movies/Browse/Dashboard';
import { UseStore } from '../Stores/BaseStore';
import './App.css';
import Navbar from './Navbar';

function App() {
  const { MovieStore } = UseStore();
  
  useEffect(() => {
    MovieStore.getMovie();
  }, [])

  return (
    <>
      <Navbar />
      <Browse />
      <Dashboard/>
    </>
  )
}

export default observer(App)
