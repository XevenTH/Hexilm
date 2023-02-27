import axios from 'axios'
import { useEffect, useState } from 'react'
import { Movie } from '../model/movie';
import './App.css'
import Dashboard from '../../Components/Movies/Dashboard/Dashboard';
import Navbar from './Navbar';
import Browse from '../../Components/Movies/Browse/Browse';
import uuid from 'react-uuid';
import { observer } from 'mobx-react-lite';
import { UseStore } from '../Stores/BaseStore';

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
