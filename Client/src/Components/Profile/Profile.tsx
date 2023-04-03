import './css/Profile.css'

import { observer } from 'mobx-react-lite'
import { useEffect, useState } from 'react'
import { UseStore } from '../../App/Stores/BaseStore'
import dataLinks from './dataLinks'
import BottomNav from './BottomNav'

export default observer(function Profile() {
  const {
    UserStore: { User },
    UserStore: {}, ProfileStore: {getProfile, profile}
  } = UseStore()

  useEffect(() => {
    try {
      if (User?.username) {
        getProfile(User?.username)
      }
    } catch (error) {
      console.log(error);
    }
    
  },[]);

  const [currentPage, setCurrentPage] = useState(0)

  const handlePageChange = (index: number) => {
    setCurrentPage(index)
  }

  const pageLinks = dataLinks()

  useEffect(() => {
    document.body.style.background = '#181823'
    document.body.style.minHeight = '100vh'

    // Cleanup function to reset the styles on unmount
    return () => {
      document.body.style.background = ''
      document.body.style.minHeight = ''
    }
  }, [])

  return (
    <>
      <div className="grid md:grid-cols-4 min-h-screen">
        <BottomNav
          currentPage={currentPage}
          handlePageChange={setCurrentPage}
        />
        <div className="bg-black/40 w-full flex justify-center md:items-baseline items-center md:pt-5">
          <div>
            <img
              src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8dXNlciUyMHByb2ZpbGV8ZW58MHx8MHx8&w=1000&q=80"
              alt=""
              width={200}
              className="rounded-lg mb-5"
            />
            <h2 className="text-white text-xl text-center mb-5">
              {profile ? profile?.displayName : 'DisplayName'}
            </h2>
          </div>
        </div>
        <div className="md:col-span-2 mt-5">{pageLinks[currentPage].page}</div>
        <div className="hidden md:block border-l border-white/30">
          <ul className="text-white py-5 tabBar">
            {pageLinks.map((data, i) => {
              return (
                <li key={i} onClick={() => handlePageChange(i)}>
                  <p>{data.name}</p>
                </li>
              )
            })}
          </ul>
        </div>
      </div>
    </>
  )
})
