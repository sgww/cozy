#include "stdafx.h"
#include "CozyKnightCore.h"
#include <vector>
#include <algorithm>

CozyKnightCore::CozyKnightCore()
    :m_hTarget(NULL)
{

}

CozyKnightCore::~CozyKnightCore()
{
    Detch();
}

void CozyKnightCore::Attch(HANDLE hProcess)
{
    Detch();

    m_hTarget = hProcess;
}

void CozyKnightCore::Detch()
{
    if(m_hTarget != NULL)
    {
        ::CloseHandle(m_hTarget);
        m_hTarget = NULL;
    }
}

BOOL CozyKnightCore::SearchFirst(const IMemoryTester& tester, DWORD dwSize, ITask& taskResult)
{
    MEMORY_BASIC_INFORMATION mbi;
    ::ZeroMemory(&mbi, sizeof(mbi));

    std::vector<BYTE> vecData;
    LPBYTE lpMemAddress     = 0;
    BOOL bReadRet           = FALSE;

    while(::VirtualQueryEx(m_hTarget, lpMemAddress, &mbi, sizeof(mbi)))
    {
        if(mbi.Type == MEM_PRIVATE && (mbi.Protect & PAGE_READWRITE) && mbi.State == MEM_COMMIT)
        {
            vecData.resize(mbi.RegionSize);
            if(::ReadProcessMemory(m_hTarget, lpMemAddress, &vecData[0], mbi.RegionSize, NULL))
            {
                for(DWORD i = 0; i < mbi.RegionSize; i+=dwSize)
                {
                    if(tester.TestMem(&vecData[i]))
                    {
                        taskResult.AddAddress(AddressInfo(m_hTarget, lpMemAddress + i));
                    }
                }
            }
        }
        lpMemAddress += mbi.RegionSize;
    }
    return true;
}

BOOL CozyKnightCore::Search(ITask& taskSource, const IProcessMemoryTester& tester)
{
    if(taskSource.GetLength() == 0)
    {
        return FALSE;
    }
    taskSource.ApplyFilter(tester);

    return TRUE;
}