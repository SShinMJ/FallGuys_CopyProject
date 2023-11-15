package com.example.fallguys.repository;

import com.example.fallguys.domain.User;
import com.example.fallguys.domain.UserCostumeColor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserCostumeColorRepository extends JpaRepository<UserCostumeColor, Long> {

    List<UserCostumeColor> findByUser(User user);
}
