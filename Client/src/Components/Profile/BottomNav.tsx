import './css/ButtonNav.css'

import { useState } from 'react'
import dataLinks from './dataLinks'

interface BottomNavProps {
  currentPage: number
  handlePageChange: (index: number) => void
}

export default function MobileNav(props: BottomNavProps) {
  const [currentPage, setCurrentPage] = useState(props.currentPage)

  const handlePageChange = (index: number) => {
    setCurrentPage(index)
    props.handlePageChange(index)
  }

  const pageLinks = dataLinks()

  return (
    <div
      className={`block md:hidden fixed bottom-4 ${
        pageLinks.length < 3 && 'flex justify-center'
      } w-full px-5`}
    >
      <div className=" p-2 bg-gray-800 text-white rounded-full drop-shadow-md">
        <ul className="flex overflow-x-scroll rounded-full scrollbar-hide">
          {pageLinks.map((data, i) => {
            return (
              <li
                key={i}
                className={`${
                  i > 0 ? 'ml-2' : ''
                } px-5 p-2 bg-gray-900 rounded-full flex-shrink-0 drop-shadow-md`}
                onClick={() => handlePageChange(i)}
              >
                {data.name}
              </li>
            )
          })}
        </ul>
      </div>
    </div>
  )
}
