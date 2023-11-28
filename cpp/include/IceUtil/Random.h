//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

#ifndef ICE_UTIL_RANDOM_H
#define ICE_UTIL_RANDOM_H

#include <IceUtil/Config.h>
#include <IceUtil/Exception.h>

#ifdef ICE_CPP11_COMPILER
#   include <algorithm>
#   include <random>
#endif

namespace IceUtilInternal
{

ICE_API void generateRandom(char*, size_t);
ICE_API unsigned int random(int = 0);

#ifdef ICE_CPP11_COMPILER

template<class T>
void shuffle(T first, T last)
{
    std::random_device rd;
    std::mt19937 rng(rd());
    std::shuffle(first, last, rng);
}

#else

struct RandomNumberGenerator
{
#if (ICE_CPLUSPLUS < 201703L)
    typedef std::ptrdiff_t argument_type;
    typedef std::ptrdiff_t result_type;
#endif
    std::ptrdiff_t operator()(std::ptrdiff_t d)
    {
        return static_cast<std::ptrdiff_t>(IceUtilInternal::random(static_cast<int>(d)));
    }
};

template<class T>
void shuffle(T first, T last)
{
    RandomNumberGenerator rng;
    random_shuffle(first, last, rng);
}

#endif

}

#endif
