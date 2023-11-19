package com.example.fallguys.util;

import jakarta.servlet.http.HttpServletRequest;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.filter.GenericFilterBean;

import java.io.IOException;

@RequiredArgsConstructor
public class JwtAuthenticationFilter extends GenericFilterBean {

    private final JwtTokenProvider jwtTokenProvider;

    //필터가 요청을 처리하는 핵심 로직이 담긴 메서드
    @Override
    public void doFilter(jakarta.servlet.ServletRequest request, jakarta.servlet.ServletResponse response, jakarta.servlet.FilterChain chain) throws IOException, jakarta.servlet.ServletException {
        // 1. Request Header에서 JWT 토큰 추출
        String token = jwtTokenProvider.resolveToken((HttpServletRequest) request);

        // 2. validateToken으로 토큰 유효성 검사
        if (token != null && jwtTokenProvider.validateToken(token)) {
            // 토큰이 유효할 경우 토큰에서 Authentication 객체를 가져와 SecurityContext에 저장.
            Authentication auth = jwtTokenProvider.getAuthentication(token);
            // 해당 요청에 대한 인증 성공여부와 사용자 정보가 컨텍스트에 저장된다.
            SecurityContextHolder.getContext().setAuthentication(auth);
        }
        // 또 다른 필터가 있다면 다음 필터로 요청 전달.
        chain.doFilter(request, response);
    }
}
