import './Sidebar.css'
import React, { useState } from 'react'
import dataLinks from './dataLinks'

export default function Sidebar() {
  const [currentPage, setCurrentPage] = useState(0)

  const handlePageChange = (index: number) => {
    setCurrentPage(index)
  }

  const pageLinks = dataLinks()

  return (
    <>
      <div className={`sidebar z-10 text-white mt-16 bg-gray-800`}>
        <div className="sidebar-icon">
          {/* PERHATIAN! */}
          {/* Bagi anda yang ingin menambahkan link baru di sidebar tolong untuk ke dataLinks.tsx yang berada di dalam folder ini juga,jangan bingung dengan apa yang mau di isi masuk saja ke filenya di sana sudah di terangkan */}
          {pageLinks.map((data, i) => {
            return (
              <a
                href={`#${data.name}`}
                onClick={() => handlePageChange(i)}
                className="hover:bg-gray-900 w-full py-2 pl-3 flex items-center"
                key={i}
              >
                <i
                  className={`bi ${data.icon} mr-3 ${
                    currentPage === i && 'text-red-600'
                  }`}
                ></i>{' '}
                <p className="titleUrl">{data.name} </p>
                {currentPage === i && (
                  <div className="ml-auto w-1 h-5 rounded-l-lg bg-red-600" />
                )}
              </a>
            )
          })}
        </div>
      </div>
      <div className={`content`}>{pageLinks[currentPage].page}</div>
    </>
  )
}
